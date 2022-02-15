import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './top-bar/home/home.component';
import { StudentComponent } from './top-bar/student/student.component';
import { TeacherComponent } from './top-bar/teacher/teacher.component';
import { TestingComponent } from './top-bar/testing/testing.component';

const routes: Routes = [
  {component:TeacherComponent,path:"teacher"},
  {component:StudentComponent,path:"student"},
  {component:TestingComponent,path:"testing"},
  {component:HomeComponent,path:""}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
