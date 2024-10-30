import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  constructor(private http:HttpClient) { 

  }
  getData():Observable<any>{
    return this.http.get('api/Person');

  }
  saveData(data:any):Observable<any>{
    return this.http.post('api/Person',data);

  }
  updateData(id:number,data:any):Observable<any>{
    return this.http.put(`api/Person/${id}`,data);

  }
  deleteData(id:number):Observable<any>{
    return this.http.delete(`/api/Person/${id}`);
  }
}
