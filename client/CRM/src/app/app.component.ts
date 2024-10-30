import { Component, createComponent, OnInit, ViewChild, viewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PersonService } from './person.service';
import { CommonModule } from '@angular/common';

interface Person{
  id:number;
  firstName:string;
  lastname:string;
  email:string;
  editing:boolean;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,FormsModule,ReactiveFormsModule,CreateComponent,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'CRM';
  public persons:Person[]=[];
  public isadd:boolean=false;
  @ViewChild(createComponent) createChild:CreateComponent |undefined;
  constructor(private personService:PersonService){}
  ngOnInit(): void {
    this.gets();
  }
  onAdd(){
    this.isadd=this.isadd?false:true;
  }
  IsEdit(isEdit:boolean,person:Person){
person.editing=true;
  }
  onEdit(isEdit:boolean,person:Person,id:number){
    person.editing=isEdit;
    this.personService.updateData(person.id,person).subscribe(data=>{ this.gets();
     
    });
  }
  gets(){
    this.personService.getData().subscribe(data => {
      this.persons=data;
    });
  }
  onclear(){
    this.createChild?.personForm.reset();
    this.isadd=false;
    this.gets();
  }
  ondelete(id:number){
    if(confirm("Are you sure want to delete this record?")){
      this.personService.deleteData(id).subscribe((data:any)=>{
        this.gets();
      });
    }
  }
}
