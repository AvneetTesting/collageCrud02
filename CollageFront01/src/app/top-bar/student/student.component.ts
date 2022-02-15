import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CollegeService } from 'src/app/services/college.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.scss']
})
export class StudentComponent implements OnInit {
  students:any;
  courses:any;
  studentDetails=new FormGroup({
    name:new FormControl(),
    courseId:new FormControl(),
    id:new FormControl(),
    studentId:new FormControl()

  });
  UpTeacher:any

  constructor(private collageApiDetails:CollegeService, private fb: FormBuilder) {
    this.studentDetails = fb.group({
      name: [""],
      courseId:0,
      id:0,
      studentId:0
    });
  }

  ngOnInit(): void {
    this.studentsdetails()
    this.coursesDetails()

  }

  // ngOnChange():void{
  //   debugger
  //   this.teachersdetails()
  // }
  studentsdetails()
  {
    // debugger
    this.collageApiDetails.GetAllStudents().subscribe((data)=>{
      // console.warn("data",data);
      this.students=data;
      // console.warn(this.students);
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
  NewStudent(details:any)
  {
    // debugger
    // console.warn(details);
    // console.warn(this.newTeacher);
    
    // console.warn(this.newTeacher);
    this.collageApiDetails.SaveNewStudent(details).subscribe((dataa)=>{
      console.warn("dataa",details);
      this.studentsdetails()
    });
    // this.ngOnInit()
    
  }
  deleteStudent(studentId:any)
  {
    // debugger
    // console.warn(teacherId);
    this.collageApiDetails.DeleteStudent(studentId).subscribe((data)=>{
    console.warn("data",data);
    this.studentsdetails();
    });
    // this.studentsdetails();
    // this.ngOnInit()

  }
  EditStudent(detail:any){
    // debugger
    console.warn(detail);
    // const{teacher}=detail;
    // this.UpTeacher=detail
    this.studentDetails.patchValue({
      name:detail.student.name,
      courseId:detail.courseId,
      id:detail.id,
      studentId:detail.studentId
    });
    // console.warn(this.teacherDetails.value);
    
    
    
    
    
    // const { teacher } = detail;
    // const {id}=detail
    // this.teacherDetails.patchValue(teacher);
    // this.teacherDetails.patchValue(id)
    // console.warn(this.teacherDetails);

    
  }

  UpdateStudent(){
    // debugger
    console.warn(this.studentDetails.value);
    this.collageApiDetails.ModifyStudent(this.studentDetails.value).subscribe(
      (response)=>{
        console.warn("response",response);
        this.studentsdetails();

      });
    // this.studentsdetails();
  }

}
