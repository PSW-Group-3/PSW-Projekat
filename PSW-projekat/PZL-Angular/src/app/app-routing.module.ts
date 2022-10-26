import { NgModule } from "@angular/core";
import { BloodBanksComponent } from './modules/blood-bank/blood-banks/blood-banks.component';
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./modules/pages/home/home.component";

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  {path: 'blood-banks', component: BloodBanksComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
