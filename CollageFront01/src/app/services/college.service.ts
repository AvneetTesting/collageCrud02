import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CollegeService {

  constructor(private httpclient:HttpClient) { }

GetAllCourses(){
   // debugger
   return this.httpclient.get("https://localhost:44397/api/Courses")
    
}

// teachers
GetAllTeachers(){
  // debugger
  return this.httpclient.get("https://localhost:44397/api/Teachers")
  
}

SaveNewTeacher(data:any)
{
  return this.httpclient.post("https://localhost:44397/api/Teachers",data)
}
DeleteTeacher(teacherId:any)
{
  return this.httpclient.delete("https://localhost:44397/api/Teachers?teacherId="+teacherId)
}
ModifyTeacher(teacherData:any)
{
  // debugger
  return this.httpclient.patch("https://localhost:44397/api/Teachers",teacherData)
}

// students

GetAllStudents(){
  // debugger
  return this.httpclient.get("https://localhost:44397/api/Students")
  
}

SaveNewStudent(data:any)
{
  return this.httpclient.post("https://localhost:44397/api/Students",data)
}
DeleteStudent(studentId:any)
{
  return this.httpclient.delete("https://localhost:44397/api/Students?studentId="+studentId)
}
ModifyStudent(teacherData:any)
{
  // debugger
  return this.httpclient.patch("https://localhost:44397/api/Students",teacherData)
}

SaveNewStudentTesting(data:any)
{
  return this.httpclient.post("https://localhost:44397/api/StudentTesting",data)
}

login(loginDetails:any)
{
  debugger
  return this.httpclient.post("https://localhost:44397/api/Users/Login",loginDetails)
}


}
