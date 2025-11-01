import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor,} from './interceptors/auth.interceptor';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { AlertCircle, BarChart3, Bell, Calendar, CheckCircle, CheckSquare, ChevronDown, ChevronLeft, ChevronRight, Clock, Edit, Filter, Github, HelpCircle, Inbox, LayoutDashboard, ListChecks, Loader2, LogOut, LucideAngularModule, Menu, Plus, Search, Settings, Trash2, TrendingUp, User, UserPlus, Users } from 'lucide-angular';
import { provideAnimations } from '@angular/platform-browser/animations';

const lucideIcons = {
  CheckCircle, ListChecks, Clock, TrendingUp, LayoutDashboard,
  CheckSquare, User, LogOut, ChevronLeft, ChevronRight, Inbox,
  Plus, Edit, Trash2, Filter, Menu, Search, Bell, ChevronDown,
  Settings, Calendar, BarChart3, Users, HelpCircle,
  UserPlus, Loader2, AlertCircle, Github
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideClientHydration(),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideAnimations(),
    importProvidersFrom(LucideAngularModule.pick(lucideIcons)),
  ]
};
