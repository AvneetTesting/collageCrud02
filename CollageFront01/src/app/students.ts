export class Students {
    id:number;
    name:any=null;
    // courseInfo:Course=new Course()
    courseInfo:Course[]=[]
    // test:any[]=[{id:0,name:""}]
    // test:Course[]=[]
    constructor(){
        this.id=0,
        this.name=""
    }
}
class Course{
    Id:any;
    name:any
    constructor(){
        this.Id=null,
        this.name=null
    }
}

