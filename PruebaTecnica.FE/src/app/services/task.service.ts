import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateTask, Task, TaskStats, UpdateTask } from '../models/task.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private apiUrl = `${environment.apiUrl}/tasks`;

  constructor(private http: HttpClient) { }

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }

  getTasksByStatus(status: boolean): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.apiUrl}/status/${status}`);
  }

  getTaskById(id: number): Observable<Task> {
    return this.http.get<Task>(`${this.apiUrl}/${id}`);
  }

  createTask(task: CreateTask): Observable<Task> {
    const requestBody = {
        title: task.title,
        description: task.description,
        dueDate: task.dueDate || null,
        priority: Number(task.priority)
    };
    return this.http.post<Task>(this.apiUrl, requestBody);
  }

  updateTask(id: number, task: UpdateTask): Observable<Task> {
    const requestBody = {
        title: task.title,
        description: task.description,
        status: task.status,
        dueDate: task.dueDate || null,
        priority: Number(task.priority)
    };
    return this.http.put<Task>(`${this.apiUrl}/${id}`, requestBody);
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getTaskStats(): Observable<TaskStats> {
    return this.http.get<TaskStats>(`${this.apiUrl}/stats`);
  }
}
