import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule 
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  activeMenu: string = 'dashboard';
  isNavbarCollapsed: boolean = true;
  isDropdownOpen: boolean = false;

  setActiveMenu(menu: string): void {
    this.activeMenu = menu;
    this.isDropdownOpen = false;
    this.isNavbarCollapsed = true; 
  }

  toggleNavbar(): void {
    this.isNavbarCollapsed = !this.isNavbarCollapsed;
  }

  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}