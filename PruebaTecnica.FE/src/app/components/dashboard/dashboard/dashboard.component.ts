import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { LucideAngularModule } from 'lucide-angular';
import { TaskService } from '../../../services/task.service';
import { TaskStats } from '../../../models/task.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  providers: [DatePipe]
})
export class DashboardComponent implements OnInit {
  stats: TaskStats | null = null;
  isLoading = true;
  today = new Date();

  constructor(
    private taskService: TaskService,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.loadStats();
  }

  loadStats(): void {
    this.isLoading = true;
    this.taskService.getTaskStats().subscribe({
      next: (stats) => {
        this.stats = stats;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading stats:', error);
        this.isLoading = false;
      }
    });
  }

  // Método para formatear fechas
  formatDate(date: string): string {
    return this.datePipe.transform(date, 'shortDate') || '';
  }
}
