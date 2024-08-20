import { Component } from '@angular/core';
import { AddNoteRequest } from '../models/add-note-request.model';
import { NoteService } from '../services/note.service'

@Component({
  selector: 'app-add-note',
  templateUrl: './add-note.component.html',
  styleUrl: './add-note.component.css'
})
export class AddNoteComponent {

  model: AddNoteRequest;
  
  constructor(private noteService: NoteService){
    this.model = {
      
      noteName: '',
      noteDescription: '',
      noteExpireDate: ''
    };
  }

  onFormSbmit(){
    console.log(this.model);
  }
}
