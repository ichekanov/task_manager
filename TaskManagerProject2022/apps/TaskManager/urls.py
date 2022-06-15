from django.urls import path

from . import views

urlpatterns = [
    path('', views.index, name='index'),
    path('new/', views.indexnew, name='indexnew'),
    path('test/', views.test, name='test')
]
