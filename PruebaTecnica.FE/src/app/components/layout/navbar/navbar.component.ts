import { Component, EventEmitter, Output } from '@angular/core';
import { User } from '../../../models/user.model';
import { AuthService } from '../../../services/auth.service';
import { LucideAngularModule } from "lucide-angular";
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    CommonModule,
    LucideAngularModule,
    RouterModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

  @Output() toggleSidebar = new EventEmitter<void>();
  currentUser: User | null = null;
  isProfileMenuOpen = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
  }

  onToggleSidebar(): void {
    this.toggleSidebar.emit();
  }

  toggleProfileMenu(): void {
    this.isProfileMenuOpen = !this.isProfileMenuOpen;
  }

  logout(): void {
    this.authService.logout();
    this.isProfileMenuOpen = false;
    this.router.navigate(['/login']);
  }

  get userInitials(): string {
    if (!this.currentUser) return 'U';
    return `${this.currentUser.firstName.charAt(0)}${this.currentUser.lastName.charAt(0)}`.toUpperCase();
  }

  get fullName(): string {
    if (!this.currentUser) return 'Usuario';
    return `${this.currentUser.firstName} ${this.currentUser.lastName}`;
  }
}
