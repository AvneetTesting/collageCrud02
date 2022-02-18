import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CollegeService } from '../services/college.service';

@Component({
  selector: 'app-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.scss']
})
export class TopBarComponent implements OnInit {
loginDetails=new FormGroup({
      UserName:new FormControl(),
      Password:new FormControl()
});
  constructor(private collegeService:CollegeService) { }

  ngOnInit(): void {
  }
  onClickSubmit()
  {
    debugger
    console.warn(this.loginDetails.value);
this.collegeService.login(this.loginDetails.value).subscribe(
  (response)=>{
    console.warn("response",response);
    
  },
  (error)=>{
    console.warn("error",error);
    
  }
  );
    
  }

}
