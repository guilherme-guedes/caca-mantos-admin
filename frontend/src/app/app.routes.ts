import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ListagemLojasComponent } from './pages/lojas/listagem-lojas/listagem-lojas.component';
import { NovaLojaComponent } from './pages/lojas/nova-loja/nova-loja.component';
import { EdicaoLojaComponent } from './pages/lojas/edicao-loja/edicao-loja.component';
import { ListagemTimesComponent } from './pages/times/listagem-times/listagem-times.component';
import { NovoTimeComponent } from './pages/times/novo-time/novo-time.component';
import { EdicaoTimeComponent } from './pages/times/edicao-time/edicao-time.component';
import { ListagemCamisasComponent } from './pages/camisas/listagem-camisas/listagem-camisas.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'lojas', component: ListagemLojasComponent },
  { path: 'lojas/nova-loja', component: NovaLojaComponent },
  { path: 'lojas/:id', component: EdicaoLojaComponent },
  { path: 'times', component: ListagemTimesComponent },
  { path: 'times/novo-time', component: NovoTimeComponent },
  { path: 'times/:id', component: EdicaoTimeComponent },
  { path: 'camisas', component: ListagemCamisasComponent }
];
