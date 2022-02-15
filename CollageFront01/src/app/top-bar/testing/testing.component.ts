import { Component, OnInit } from '@angular/core';
import { CollegeService } from 'src/app/services/college.service';
import { Students } from 'src/app/students';

@Component({
  selector: 'app-testing',
  templateUrl: './testing.component.html',
  styleUrls: ['./testing.component.scss']
})
export class TestingComponent implements OnInit {
studentList:Students[]=[];
newStudent:Students=new Students()
selectedCoursesList:any[]=[]
courses:any;
students:any;
  constructor(private collegeService:CollegeService) { }

  ngOnInit(): void {
    this.CoursesList()
    this.studentsdetails()
  }
  CoursesList()
  {
    this.collegeService.GetAllCourses().subscribe(
      (response)=>{
        console.warn(response);
        this.courses=response
      });
  }
  SaveClick(){
    debugger
    console.warn(this.newStudent);
    
    
    
    this.collegeService.SaveNewStudentTesting(this.newStudent).subscribe(
      (response)=>{
        console.warn(response)
        this.EmptyRec()
        this.SelectItem()
      }
    );
    
  }
  EmptyRec()
  {
    // this.newStudent.name=""
    // this.newStudent.courseInfo.Id=null
    // this.newStudent.courseInfo.name=null
    this.newStudent.courseInfo=[]
    this.newStudent.name=""
  }
  SelectItem()
  {
    debugger
    this.newStudent.courseInfo.forEach(element => {
      console.warn(name);
      
    });
    // alert("working")
    // this.selectedCoursesList=this.newStudent.courseInfo
  }
  studentsdetails()
  {
    // debugger
    this.collegeService.GetAllStudents().subscribe((data)=>{
      // console.warn("data",data);
      this.students=data;
      // console.warn(this.students);
    })
  }
}
