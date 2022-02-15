import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { StudentComponent } from './top-bar/student/student.component';
import { TeacherComponent } from './top-bar/teacher/teacher.component';
import { HomeComponent } from './top-bar/home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { TestingComponent } from './top-bar/testing/testing.component'

@NgModule({
  declarations: [
    AppComponent,
    TopBarComponent,
    StudentComponent,
    TeacherComponent,
    HomeComponent,
    TestingComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
