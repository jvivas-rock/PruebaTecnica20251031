import { Component } from '@angular/core';
import { SidebarComponent } from "./sidebar/sidebar.component";
import { NavbarComponent } from "./navbar/navbar.component";
import { RouterOutlet } from "@angular/router";
import { FooterComponent } from "./footer/footer.component";

@Component({
  selector: 'app-layout',
  standalone: true,
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
  imports: [SidebarComponent, NavbarComponent, RouterOutlet, FooterComponent]
})
export class LayoutComponent {
  isSidebarCollapsed = false;

  onToggleSidebar(): void {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
  }
}
