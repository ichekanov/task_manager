'''Create your views here.'''
from datetime import datetime
from django.utils import timezone
from django.shortcuts import render, redirect
from django.http import HttpResponse
from .models import Task, List, Settings

CHOSEN_TASK = 0
CHOSEN_LIST = 1


def filter_task_list():
    '''forms task list according to sort settings'''
    order = Settings.objects.all()[0].tasks_order
    reversed_order = Settings.objects.all()[0].reversed_order
    try:
        if reversed_order:
            if order == Settings.BY_CHRONO:
                task_list = Task.objects.filter(
                    list_id=CHOSEN_LIST, in_list=True).order_by('-task_id')
            elif order == Settings.BY_CREATE:
                task_list = Task.objects.filter(
                    list_id=CHOSEN_LIST, in_list=True).order_by('-name')
            elif order == Settings.BY_DONE:
                task_list = Task.objects.filter(
                    list_id=CHOSEN_LIST, in_list=True).order_by('deadline', 'name')
        else:
            if order == Settings.BY_CHRONO:
                task_list = Task.objects.filter(
                    list_id=CHOSEN_LIST, in_list=True)
            elif order == Settings.BY_CREATE:
                task_list = Task.objects.filter(
                    list_id=CHOSEN_LIST, in_list=True).order_by('name')
            elif order == Settings.BY_DONE:
                task_list = Task.objects.filter(
                    list_id=CHOSEN_LIST, in_list=True).order_by('-deadline', 'name')
    except Task.DoesNotExist:
        task_list = []
    return task_list


def process_req(req):
    '''Processes the POST request from interface'''
    global CHOSEN_TASK
    global CHOSEN_LIST
    if '_done' in req:
        tid = int(req['_done'])
        task = Task.objects.get(task_id=tid)
        task.set_done(not task.done)
        return 0
    if '_set' in req:
        CHOSEN_TASK = int(req['_set'])
        task = Task.objects.get(task_id=CHOSEN_TASK)
        task.in_list = True
        return 0
    if '_set_list' in req:
        CHOSEN_LIST = int(req['_set_list'])
        return 0
    if '_add' in req:
        Task().create(req['name'], CHOSEN_LIST)
        return 0
    if '_add_subtask' in req:
        tid = int(req['parent_id'])
        task = Task.objects.get(task_id=tid)
        task.add_subtask(req['name_field'])
        return 0
    if '_add_list' in req:
        List().create(req['name'])
        return 0
    if '_show' in req:
        tid = int(req['_show'])
        task = Task.objects.get(task_id=tid)
        task.set_in_list(not task.in_list)
        return 0
    if '_rename' in req:
        tid = int(req['task_id'])
        task = Task.objects.get(task_id=tid)
        task.set_name(req['_rename'])
        return 0
    if '_set_description' in req:
        tid = int(req['task_id'])
        task = Task.objects.get(task_id=tid)
        task.set_description(req['_set_description'])
        return 0
    if '_set_deadline' in req:
        tid = int(req['task_id'])
        task = Task.objects.get(task_id=tid)
        deadline = datetime.strptime(req['_set_deadline'], "%Y-%m-%d")
        task.set_deadline(deadline)
        return 0
    if '_delete' in req:
        tid = int(req['_delete'])
        task = Task.objects.get(task_id=tid)
        if task.parent == 0 or task.in_list:
            CHOSEN_TASK = 0
        task.delete_task()
        return 1
    if '_delete_list' in req:
        lid = int(req['_delete_list'])
        lst = List.objects.get(list_id=lid)
        lst.delete_list()
        CHOSEN_LIST = List.objects.all()[0].list_id
        return 1
    if '_search' in req:
        Settings.SEARCH_STRING = req['_search']
        print("Recieved NEW SEARCH STRING")
        return 1


# по хорошему, следующие два поля нужно вынести в базу данных, чтобы сохранять настройки между сессиями
show_done = True
order = 0
# 0 - по порядку добавления
# 1 - по алфавиту
# 2 - по дате выполнения

def index(request):
    '''Main page generator'''
    global CHOSEN_TASK
    global CHOSEN_LIST
    if len(Settings.objects.all()) == 0:
        Settings().create_config()
    if len(List.objects.all()) == 0:
        List().create('Задачи')
    show_done = Settings.objects.all()[0].show_done_tasks
    if request.method == 'POST':
        # buttons and fields handler
        print(request.POST)
        if process_req(request.POST):
            return redirect('/')
    if CHOSEN_TASK:
        selected_task = Task.objects.get(task_id=CHOSEN_TASK)
        stid = Task.objects.get(task_id=CHOSEN_TASK).subtasks['subtasks']
        subtasks = Task.objects.filter(task_id__in=stid)
        selected_day = str(selected_task.deadline.day).zfill(2)
        selected_month = str(selected_task.deadline.month).zfill(2)
    else:
        selected_task = None
        subtasks = []
        selected_day = None
        selected_month = None
    args = {'task_list': filter_task_list(),
            'selected_task': selected_task,
            'subtasks': subtasks,
            'nodate': datetime.fromtimestamp(0, tz=timezone.utc),
            'selected_month': selected_month,
            'selected_day': selected_day,
            'lists_list': List.objects.all(),
            'selected_list': List.objects.get(list_id=CHOSEN_LIST),
            'show_done': show_done,
            'search_string': Settings.SEARCH_STRING
            }
    return render(request, 'TaskManager/main.html', args)

def indexnew(request):
    '''Main page generator'''
    global CHOSEN_TASK
    global CHOSEN_LIST
    if len(Settings.objects.all()) == 0:
        Settings().create_config()
    if len(List.objects.all()) == 0:
        List().create('Задачи')
    show_done = Settings.objects.all()[0].show_done_tasks
    if request.method == 'POST':
        # buttons and fields handler
        print(request.POST)
        if process_req(request.POST):
            return redirect('/new')
    if CHOSEN_TASK:
        selected_task = Task.objects.get(task_id=CHOSEN_TASK)
        stid = Task.objects.get(task_id=CHOSEN_TASK).subtasks['subtasks']
        subtasks = Task.objects.filter(task_id__in=stid)
        selected_day = str(selected_task.deadline.day).zfill(2)
        selected_month = str(selected_task.deadline.month).zfill(2)
    else:
        selected_task = None
        subtasks = []
        selected_day = None
        selected_month = None
    args = {'task_list': filter_task_list(),
            'selected_task': selected_task,
            'subtasks': subtasks,
            'nodate': datetime.fromtimestamp(0, tz=timezone.utc),
            'selected_month': selected_month,
            'selected_day': selected_day,
            'lists_list': List.objects.all(),
            'selected_list': List.objects.get(list_id=CHOSEN_LIST),
            'show_done': show_done
            }
    return render(request, 'TaskManager/mainnew.html', args)


def test(request):
    '''Test page geneartor'''
    return HttpResponse("Test")

#! сейчас способ добавления задач не оптимален, т.к. в форму можно ввести HTML код
# но получение данных из task_form плохо, т.к. возвращаются
# данные как в комментарии:
# <tr>
#     <th><label for="id_name">Название задачи:</label></th>
#     <td>
#       <input type="text" name="name" value="Новая задача" maxlength="250" id="id_name">
#     </td>
# </tr>
