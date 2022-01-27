import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { AdminGuard } from "../shared/guards/admin.guard";

const routes: Routes = [
  { path: '', component: ListComponent, pathMatch: 'full' },
  { path: 'cars/list', component: ListComponent },
  { path: ':carId/details', component: DetailsComponent },
  { path: 'create', component: CreateComponent, canActivate: [AdminGuard] },
  { path: ':carId/edit', component: EditComponent, canActivate: [AdminGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarsRoutingModule { }
