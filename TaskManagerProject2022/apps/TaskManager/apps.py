from django.apps import AppConfig


class TaskManagerConfig(AppConfig):
    default_auto_field = 'django.db.models.BigAutoField'
    # name = 'TaskManagerProject2022.apps.TaskManager' # Fixed: Model class doesn't declare an explicit app_label
    name = 'TaskManager'                               # and isn't in an application in INSTALLED_APPS.
    verbose_name = 'Таск-Менеджер'
