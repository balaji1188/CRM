import { Component, EventEmitter, Output, output } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PersonService } from '../person.service';


@Component({
  selector: 'app-create',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent {

  personForm= new FormGroup({
    id:new FormGroup(''),
    firstName:new FormGroup(''),
    lastName:new FormGroup(''),
    email:new FormGroup(''),
  });
  constructor(private personService:PersonService){}
   @Output() onCloseClick=new EventEmitter();
  fromRestClicked:boolean=false;
  onSubmit(){
    this.personService.saveData(this.personForm.value).subscribe((data:any)=>{
      this.onCloseClick.emit();
    });
  }
  onclear(){
    this.personForm.reset();
    this.onCloseClick.emit();
  }
}