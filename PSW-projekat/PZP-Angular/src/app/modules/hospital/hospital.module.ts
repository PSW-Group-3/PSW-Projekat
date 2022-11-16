import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "src/app/material/material.module";
import { CreateRoomComponent } from "./create-room/create-room.component";
import { RoomDetailComponent } from "./room-detail/room-detail.component";
import { RoomsComponent } from "./rooms/rooms.component";
import { UpdateRoomComponent } from "./update-room/update-room.component";
import { FeedbacksComponent } from './feedbacks/feedbacks.component';
import { CreateFeedbackComponent } from './create-feedback/create-feedback.component';
import { WelcomeComponent } from "./welcome/welcome.component";
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: 'rooms', component: RoomsComponent },
  { path: 'rooms/add', component: CreateRoomComponent },
  { path: 'rooms/:id', component: RoomDetailComponent },
  { path: 'rooms/:id/update', component: UpdateRoomComponent },
  { path: 'feedbacks', component: FeedbacksComponent },
  { path: 'feedbacks/add', component: CreateFeedbackComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];

@NgModule({
  declarations: [
    RoomsComponent,
    RoomDetailComponent,
    CreateRoomComponent,
    UpdateRoomComponent,
    FeedbacksComponent,
    CreateFeedbackComponent,
    WelcomeComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ],
  exports: [ RouterModule ]
})
export class HospitalModule { }
