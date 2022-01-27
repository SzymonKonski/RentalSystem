import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { DealerlistComponent } from './dealerlist/dealerlist.component';
import { AdminGuard } from "../shared/guards/admin.guard";

const routes: Routes = [
  { path: '', component: DealerlistComponent, pathMatch: 'full'},
  { path: 'create', component: CreateComponent, canActivate: [AdminGuard] },
  { path: 'dealers/list', component: DealerlistComponent },
  { path: ':dealerId/edit', component: EditComponent, canActivate: [AdminGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DealerRoutingModule { }
