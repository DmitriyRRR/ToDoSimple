import { Injectable } from '@angular/core';
import { AddNoteComponent } from '../add-note/add-note.component';
import { AddNoteRequest } from '../models/add-note-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http'; 


@Injectable({
  providedIn: 'root'
})
export class NoteService {

  constructor(private http:HttpClient) { }

  addNote(model:AddNoteRequest):Observable<void>{
return this.http.post<void>(`https://localhost:7155/home/Create`,model);
  }
}
