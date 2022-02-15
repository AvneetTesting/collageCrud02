import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CollegeService } from 'src/app/services/college.service';

@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html',
  styleUrls: ['./teacher.component.scss']
})
export class TeacherComponent implements OnInit {
  teachers:any;
  courses:any;
  teacherDetails=new FormGroup({
    name:new FormControl(),
    courseId:new FormControl(),
    id:new FormControl(),
    teacherId:new FormControl(),

  });
  UpTeacher:any

  constructor(private collageApiDetails:CollegeService, private fb: FormBuilder) {
    this.teacherDetails = fb.group({
      name: [""],
      courseId:0,
      id:0,
      teacherId:0
    });
  }

  ngOnInit(): void {
    this.teachersdetails()
    this.coursesDetails()

  }

  // ngOnChange():void{
  //   debugger
  //   this.teachersdetails()
  // }
  teachersdetails()
  {
    // debugger
    this.collageApiDetails.GetAllTeachers().subscribe((data)=>{
      // console.warn("data",data);
      this.teachers=data;
      // console.warn(this.teachers);
    })
  }
  coursesDetails()
  {
    // debugger
    this.collageApiDetails.GetAllCourses().subscribe((data)=>{
      // console.warn("data",data);      
      this.courses=data;
    })
  }
  NewTeacher(details:any)
  {
    // debugger
    // console.warn(details);
    // console.warn(this.newTeacher);
    
    // console.warn(this.newTeacher);
    this.collageApiDetails.SaveNewTeacher(details).subscribe((dataa)=>{
      console.warn("dataa",details);
      this.teachersdetails();
    });
    // this.ngOnInit()
    // this.teachersdetails()
  }
  deleteTeacher(teacherId:any)
  {
    // debugger
    // console.warn(teacherId);
    this.collageApiDetails.DeleteTeacher(teacherId).subscribe((data)=>{
    console.warn("data",data);
    this.teachersdetails();

    });

  }
  EditTeacher(detail:any){
    // debugger
    console.warn(detail);
    // const{teacher}=detail;
    this.UpTeacher=detail
    this.teacherDetails.patchValue({
      name:detail.teacher.name,
      courseId:detail.courseId,
      id:detail.id,
      teacherId:detail.teacherId
    });
    // console.warn(this.teacherDetails.value);
    
    
    
    
    
    // const { teacher } = detail;
    // const {id}=detail
    // this.teacherDetails.patchValue(teacher);
    // this.teacherDetails.patchValue(id)
    // console.warn(this.teacherDetails);

    
  }

  UpdateTeacher(){
    // debugger
    console.warn(this.teacherDetails.value);
    this.collageApiDetails.ModifyTeacher(this.teacherDetails.value).subscribe(
      (response)=>{
        console.warn("response",response);
        this.teachersdetails();
      });

  }

}
