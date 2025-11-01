import { Component, OnInit } from '@angular/core';
import { CreateTask, Task, TaskPriority, TaskPriorityColors, TaskPriorityLabels, UpdateTask } from '../../../models/task.model';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TaskService } from '../../../services/task.service';
import { CommonModule } from '@angular/common';
import { LucideAngularModule } from 'lucide-angular';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    LucideAngularModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss'
})
export class TasksComponent implements OnInit{

  tasks: Task[] = [];
  filteredTasks: Task[] = [];
  selectedTask: Task | null = null;
  isCreateModalOpen = false;
  isEditModalOpen = false;
  isDeleteModalOpen = false;
  isLoading = false;
  currentFilter: 'all' | 'pending' | 'completed' = 'all';
  searchTerm = '';

  taskForm: FormGroup;

  TaskPriority = TaskPriority

  constructor(
    private taskService: TaskService,
    private fb: FormBuilder
  ) {
    this.taskForm = this.createTaskForm();
  }

  ngOnInit(): void {
    this.loadTasks();
  }

  private createTaskForm(): FormGroup {
    return this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(5)]],
      dueDate: [''],
      priority: [TaskPriority.Medium, Validators.required]
    });
  }

  loadTasks(): void {
    this.isLoading = true;
    this.taskService.getTasks().subscribe({
      next: (tasks) => {
        this.tasks = tasks;
        this.applyFilters();
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading tasks:', error);
        this.isLoading = false;
      }
    });
  }

    applyFilters(): void {
    let filtered = this.tasks;

    // Aplicar filtro de estado
    if (this.currentFilter === 'pending') {
      filtered = filtered.filter(task => !task.status);
    } else if (this.currentFilter === 'completed') {
      filtered = filtered.filter(task => task.status);
    }

    // Aplicar búsqueda
    if (this.searchTerm) {
      const term = this.searchTerm.toLowerCase();
      filtered = filtered.filter(task =>
        task.title.toLowerCase().includes(term) ||
        task.description.toLowerCase().includes(term)
      );
    }

    this.filteredTasks = filtered;
  }

  onFilterChange(filter: 'all' | 'pending' | 'completed'): void {
    this.currentFilter = filter;
    this.applyFilters();
  }

  onSearchChange(term: string): void {
    this.searchTerm = term;
    this.applyFilters();
  }

  openCreateModal(): void {
    this.taskForm.reset({
      title: '',
      description: '',
      dueDate: '',
      priority: TaskPriority.Medium
    });
    this.isCreateModalOpen = true;
  }

  openEditModal(task: Task): void {
    this.selectedTask = task;
    this.taskForm.patchValue({
      title: task.title,
      description: task.description,
      dueDate: task.dueDate ? task.dueDate.split('T')[0] : '',
      priority: task.priority
    });
    this.isEditModalOpen = true;
  }

  openDeleteModal(task: Task): void {
    this.selectedTask = task;
    this.isDeleteModalOpen = true;
  }

  closeModals(): void {
    this.isCreateModalOpen = false;
    this.isEditModalOpen = false;
    this.isDeleteModalOpen = false;
    this.selectedTask = null;
  }

  createTask(): void {
    if (this.taskForm.invalid) return;

    const taskData: CreateTask = this.taskForm.value;
    this.taskService.createTask(taskData).subscribe({
      next: (task) => {
        this.tasks.unshift(task);
        this.applyFilters();
        this.closeModals();
      },
      error: (error) => {
        console.error('Error creating task:', error);
      }
    });
  }

  updateTask(): void {
    if (this.taskForm.invalid || !this.selectedTask) return;

    const taskData: UpdateTask = {
      ...this.taskForm.value,
      status: this.selectedTask.status
    };

    this.taskService.updateTask(this.selectedTask.id, taskData).subscribe({
      next: (updatedTask) => {
        const index = this.tasks.findIndex(t => t.id === updatedTask.id);
        if (index !== -1) {
          this.tasks[index] = updatedTask;
          this.applyFilters();
        }
        this.closeModals();
      },
      error: (error) => {
        console.error('Error updating task:', error);
      }
    });
  }

  deleteTask(): void {
    if (!this.selectedTask) return;

    this.taskService.deleteTask(this.selectedTask.id).subscribe({
      next: () => {
        this.tasks = this.tasks.filter(t => t.id !== this.selectedTask!.id);
        this.applyFilters();
        this.closeModals();
      },
      error: (error) => {
        console.error('Error deleting task:', error);
      }
    });
  }

  toggleTaskStatus(task: Task): void {
    const updateData: UpdateTask = {
      title: task.title,
      description: task.description,
      status: !task.status,
      dueDate: task.dueDate,
      priority: task.priority
    };

    this.taskService.updateTask(task.id, updateData).subscribe({
      next: (updatedTask) => {
        const index = this.tasks.findIndex(t => t.id === updatedTask.id);
        if (index !== -1) {
          this.tasks[index] = updatedTask;
          this.applyFilters();
        }
      },
      error: (error) => {
        console.error('Error updating task status:', error);
      }
    });
  }

  getPriorityLabel(priority: TaskPriority): string {
    return TaskPriorityLabels[priority];
  }

  getPriorityColor(priority: TaskPriority): string {
    return TaskPriorityColors[priority];
  }

  get completedTasksCount(): number {
    return this.tasks.filter(task => task.status).length;
  }

  get pendingTasksCount(): number {
    return this.tasks.filter(task => !task.status).length;
  }

  get totalTasksCount(): number {
    return this.tasks.length;
  }
}
