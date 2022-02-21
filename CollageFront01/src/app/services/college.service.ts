import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CollegeService {

  // currentUserName:any="";

  constructor(private httpclient:HttpClient) { }

GetAllCourses(){
   // debugger
   return this.httpclient.get("https://localhost:44397/api/Courses")
    
}

// teachers
GetAllTeachers(){
  // debugger
     //JWT
   var currentUser={token:""};
   var headers=new HttpHeaders();
   headers=headers.set("Authorization","Bearer "); 
   var currentUserSession=sessionStorage.getItem("currentUser");
   if(currentUserSession != null)
   {
     currentUser=JSON.parse(currentUserSession);
     headers=headers.set("Authorization","Bearer " +currentUser.token);    
   }

  return this.httpclient.get("https://localhost:44397/api/Teachers",{headers:headers})
  
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
  .pipe(map(u=>{
    if(u)
    {
      //  this.currentUserName=u.username;
       sessionStorage['currentUser']=JSON.stringify(u);
       console.log(sessionStorage.getItem("currentUser"));
    }
    return u;
  }))
}

Authoriz()
{
  // return this.httpclient.post("https://localhost:44397/api/Users/Login")
}


}
