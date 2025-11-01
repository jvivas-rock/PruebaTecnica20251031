export interface Task {
  id: number;
  taskCode: string;
  title: string;
  description: string;
  status: boolean;
  dueDate?: string;
  priority: TaskPriority;
  createdAt: string;
  updatedAt: string;
  userId: number;
}

export interface CreateTask {
  title: string;
  description: string;
  dueDate?: string;
  priority: TaskPriority;
}

export interface UpdateTask {
  title: string;
  description: string;
  status: boolean;
  dueDate?: string;
  priority: TaskPriority;
}

export interface TaskStats {
  totalTasks: number;
  completedTasks: number;
  pendingTasks: number;
  completionPercentage: number;
  recentTasks: Task[];
}

export enum TaskPriority {
  Low = 1,
  Medium = 2,
  High = 3
}

export const TaskPriorityLabels = {
  [TaskPriority.Low]: 'Baja',
  [TaskPriority.Medium]: 'Media',
  [TaskPriority.High]: 'Alta'
};

export const TaskPriorityColors = {
  [TaskPriority.Low]: 'bg-green-100 text-green-800',
  [TaskPriority.Medium]: 'bg-yellow-100 text-yellow-800',
  [TaskPriority.High]: 'bg-red-100 text-red-800'
};
