'''DB structure and methods'''


from datetime import datetime
from django.db import models
from django.utils import timezone


class Task(models.Model):
    '''Class "Задача" with fields and methods'''
    task_id = models.AutoField(primary_key=True)
    name = models.CharField('Название задачи', max_length=250)
    done = models.BooleanField('Задача выполнена')
    in_list = models.BooleanField('Задача отображается в общем списке')
    parent = models.IntegerField('Родительская подзадача')
    description = models.TextField('Заметка')
    create_date = models.DateTimeField('Дата создания задачи')
    modify_date = models.DateTimeField('Дата последнего изменения задачи')
    deadline = models.DateTimeField('Дата выполнения задачи')
    color = models.CharField('Цвет', max_length=6)
    # JSON формат для спиcка id подзадач
    subtasks = models.JSONField('Подзадачи')  # {'subtasks': [id1, id2, ...]}
    list_id = models.IntegerField('Номер родительского списка')

    class Meta:
        '''Default Django class'''
        verbose_name = 'Задача'
        verbose_name_plural = 'Задачи'

    def __str__(self):
        return self.name

    def create(self, name, list_id, in_list=True, parent=0, description="None", deadline=datetime.fromtimestamp(0, tz=timezone.utc)):
        '''Creating a task'''
        self.name = name
        self.done = False
        self.in_list = in_list
        self.parent = parent
        self.description = description
        self.create_date = datetime.now(tz=timezone.utc)
        self.modify_date = datetime.now(tz=timezone.utc)
        self.deadline = deadline
        self.color = 'E3FFF5'
        self.subtasks = {"subtasks": []}
        self.list_id = list_id
        self.save()
        return self.task_id

    def add_subtask(self, name):
        '''Creating a subtask'''
        if self.parent != 0:
            return -1
        sub_id = Task().create(name, in_list=False, parent=self.task_id, list_id=self.list_id)
        self.subtasks["subtasks"].append(sub_id)
        self.modify_date = datetime.now(tz=timezone.utc)
        self.save()
        return sub_id

    def delete_task(self):
        '''Deleting task and all the subtasks'''
        if self.parent == 0:
            for task in self.subtasks["subtasks"]:
                Task.objects.get(task_id=task).delete()
        else:
            parent_task = Task.objects.get(task_id = self.parent)
            parent_task.subtasks['subtasks'].remove(self.task_id)
            parent_task.save()
        Task.objects.get(task_id=self.task_id).delete()

    def set_name(self, text):
        '''Sets name of task'''
        self.name = text
        self.modify_date = datetime.now(tz=timezone.utc)
        self.save()

    def set_done(self, state):
        '''Sets status of task'''
        self.done = state
        self.modify_date = datetime.now(tz=timezone.utc)
        self.save()

    def set_in_list(self, state):
        '''Sets visibility of task in list of tasks'''
        self.in_list = state
        self.modify_date = datetime.now(tz=timezone.utc)
        self.save()

    def set_description(self, text):
        '''Sets description (Заметка) of task'''
        self.description = text
        self.modify_date = datetime.now(tz=timezone.utc)
        self.save()

    def set_deadline(self, time):
        '''Sets deadline of task'''
        self.deadline = time
        self.modify_date = datetime.now(tz=timezone.utc)
        self.save()

    def set_color(self, text):
        '''Sets accent color of task'''
        self.color = text
        self.modify_date = datetime.now(tz=timezone.utc)
        self.save()


class List(models.Model):
    '''Class "Список задач"'''
    list_id = models.AutoField('Номер списка', primary_key=True)
    name = models.CharField('Название списка', max_length=250)

    class Meta:
        '''Default Django class'''
        verbose_name = 'Задача'
        verbose_name_plural = 'Задачи'

    def __str__(self):
        return self.name

    def create(self, name):
        '''Create task list'''
        self.name = name
        self.save()
        return self.list_id

    def set_name(self, new_name):
        '''Sets name of existing list'''
        self.name = new_name
        self.save()
    
    def delete_list(self):
        '''Deleting list and all the tasks'''
        for task in Task.objects.filter(list_id=self.list_id):
            task.delete_task()
        self.delete()


class Settings(models.Model):
    '''App settings class'''
    SEARCH_STRING = ""
    BY_CHRONO = 0
    BY_CREATE = 1
    BY_DONE = 2
    show_done_tasks = models.BooleanField('Показывать выполненные задачи')
    sort_by = [
        (BY_CHRONO, 'Chronological'),
        (BY_CREATE, 'By creating date'),
        (BY_DONE, 'By to be done date')
    ]
    tasks_order = models.IntegerField('Порядок сортировки', choices=sort_by)
    play_sound = models.BooleanField('Проигрывать звук при выполнении')
    reversed_order = models.BooleanField(
        'Показывать задачи в обратном порядке')

    def create_config(self):
        '''initialization of db table'''
        self.show_done_tasks = True
        self.tasks_order = 0
        self.reversed_order = False
        self.play_sound = False
        self.save()

    def set_show_done_tasks(self, setting):
        '''Sets show_done_tasks setting'''
        self.show_done_tasks = setting
        self.save()

    def set_tasks_order(self, setting):
        '''Sets tasks_order setting'''
        self.tasks_order = setting
        self.save()

    def set_reversed_order(self, setting):
        '''Sets reversed_order setting'''
        self.reversed_order = setting
        self.save()

    def set_play_sound(self, setting):
        '''Sets play_sound setting'''
        self.play_sound = setting
        self.save()
