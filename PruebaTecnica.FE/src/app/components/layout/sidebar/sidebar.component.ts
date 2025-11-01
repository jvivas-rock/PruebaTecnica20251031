import { Component, EventEmitter, HostListener, OnInit, Output } from '@angular/core';
import { User } from '../../../models/user.model';
import { AuthService } from '../../../services/auth.service';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { LucideAngularModule, FileIcon } from 'lucide-angular';
import { CommonModule } from '@angular/common';

interface MenuItem {
  path: string;
  icon: string;
  label: string;
  active: boolean;
  badge?: number;
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    CommonModule,
    LucideAngularModule,
    RouterModule
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent implements OnInit{

  @Output() toggleSidebar = new EventEmitter<void>();
  isCollapsed = false;
  currentUser: User | null = null;
  isMobile = false;
  readonly FileIcon = FileIcon;

  menuItems: MenuItem[] = [
    { path: '/dashboard', icon: 'layout-dashboard', label: 'Dashboard', active: false },
    { path: '/tasks', icon: 'check-square', label: 'Tareas', active: false },
    { path: '/calendar', icon: 'calendar', label: 'Calendario (futuro)', active: false },
    { path: '/reports', icon: 'bar-chart-3', label: 'Reportes (futuro)', active: false },
    { path: '/team', icon: 'users', label: 'Equipo (futuro)', active: false }
  ];

  ngOnInit(): void {
    console.log('SidebarComponent initialized - ID:', Math.random());
    // Esto te ayudará a ver cuántas instancias se crean
  }

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateActiveMenu(event.url);
      }
    });

    this.checkScreenSize();
  }

  @HostListener('window:resize')
  onResize(): void {
    this.checkScreenSize();
  }

  private checkScreenSize(): void {
    this.isMobile = window.innerWidth < 768;
    if (this.isMobile) {
      this.isCollapsed = true;
    }
  }

  toggleCollapse(): void {
    this.isCollapsed = !this.isCollapsed;
    this.toggleSidebar.emit();
  }

  updateActiveMenu(currentUrl: string): void {
    this.menuItems.forEach(item => {
      item.active = currentUrl === item.path || currentUrl.startsWith(item.path + '/');
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  get userInitials(): string {
    if (!this.currentUser) return 'U';
    return `${this.currentUser.firstName.charAt(0)}${this.currentUser.lastName.charAt(0)}`.toUpperCase();
  }

}
