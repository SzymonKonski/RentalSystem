import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { UserService } from "../user.service";
import { EnvironmentUrlService } from "../../environment-url.service";

@Component({
  selector: 'app-return',
  templateUrl: './return.component.html',
  styleUrls: ['./return.component.css']
})

export class ReturnComponent implements OnInit {
  public progress: number;
  public message: string;
  public progress2: number;
  public message2: string;
  public valid: boolean = false;
  public valid2: boolean = false;
  file: File;
  file2: File;
  fileName: string;
  fileName2: string;
  price: number;
  //formData: FormData = new FormData();
  id: number;

  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, private route: ActivatedRoute, public userService: UserService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['rentId'];
    this.userService.getPrice(this.id).subscribe(result => {
      this.price = result;
    },
    error => console.error(error));
  }


  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    if ((fileToUpload.type != "image/jpeg") && (fileToUpload.type != "image/png")) {
      this.message = 'Choose a png or jpeg file.'
      this.valid = false;
      return;
    }
    this.valid = true;
    this.message = '';
    this.file = fileToUpload;
    this.fileName = fileToUpload.name;
  }

  public uploadFile2 = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    if (fileToUpload.type != "application/pdf") {
      this.message2 = 'Choose a PDF file.'
      this.valid2 = false;
      return;
    }
    this.valid2 = true;
    this.message2 = '';
    this.file2 = fileToUpload;
    this.fileName2 = fileToUpload.name;
  }


  public sendFiles = (files, files2) => {

    let formData = new FormData();
    formData.append('file', this.file, this.file.name);
    formData.append('file', this.file2, this.file2.name);

    this.http.post(this.envUrl.createCompleteRoute('api/reservations/ReturnMe/' + this.id), formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });


    /*this.valid = this.valid2 = false;*/
  }
}
