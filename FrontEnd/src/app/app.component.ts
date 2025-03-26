import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule, HttpHeaders, HttpParams } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HttpClientModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'], 
})
export class AppComponent implements OnInit {
  title = 'FrontEnd';
  options: { Value: number; Text: string }[] = [];
  subOptions: { Value: number; Text: string }[] = [];
  profes: { Value: number; Text: string }[] = [];
  message: string | null = null;
  alertClass: string = '';
  messageIns: string | null = null;
  alertClassIns: string = '';
  messageReg: string | null = null;
  alertClassReg: string = '';
  professors: { ProfessorName: string, Text: string }[] = [];
  students: { Student: string }[] = [];
  constructor(private httpCliente: HttpClient) {

  }
  ngOnInit(): void {
    this.LoadProfessor();
  }
  LoadProfessor() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 
    });
    this.httpCliente.get('https://localhost:7285/api/interrapidisimo/getAllProfessor', { headers })
      .subscribe((result: any) => {
        const dynamicOptions = JSON.parse(result.message);

        this.profes = [
          { Value: '', Text: 'Seleccione un profesor' },
          ...dynamicOptions
        ];
        console.log(this.options)
        this.message = '';
        this.alertClass = '';


      });
  }

  onIdentificationInsChange(event: Event) {
    this.messageIns = '';
    this.alertClassIns = '';
    const identificacion = event.target as HTMLInputElement;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 
    });
    this.httpCliente.get('https://localhost:7285/api/interrapidisimo/getStudent/' + identificacion.value, { headers })
      .subscribe((result: any) => {
        const dynamicOptions = JSON.parse(result.message);

        if (dynamicOptions.length > 0) {
          (document.getElementById('studentName') as HTMLSelectElement).value
            = dynamicOptions[0].FirstName + ' ' + dynamicOptions[0].LastName;
          (document.getElementById('idStudent') as HTMLSelectElement).value
            = dynamicOptions[0].StudentID;
          this.messageIns = '';
          this.alertClassIns = '';
        } else {
          (document.getElementById('studentName') as HTMLSelectElement).value
            = '';
          (document.getElementById('idStudent') as HTMLSelectElement).value
            = '';
          this.messageIns = 'No se encontro el estudiante';
          this.alertClassIns = 'alert-warning';
        }

      });
  }

  CreateIns() {
    this.messageIns = "";
    this.alertClassIns = '';
    let idSubject = (document.getElementById('subjectIns') as HTMLSelectElement).value;
    let idStudent = (document.getElementById('idStudent') as HTMLSelectElement).value;
    let idProfessor = (document.getElementById('professor') as HTMLSelectElement).value;

    if (idSubject != "" && idStudent != "" && idProfessor != "") {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json', 
      });
      this.httpCliente.post('https://localhost:7285/api/interrapidisimo/addSubject/' + idSubject +
        '/' + idStudent + '/' + idProfessor, { headers })
        .subscribe((result: any) => {
          if (result.errorStatus) {
            this.messageIns = result.message;
            this.alertClassIns = 'alert-warning';
          } else {
            this.messageIns = result.message;
            this.alertClassIns = 'alert-success';
          }

        });
    }
    else {
      this.messageIns = "Todos los campos son obligatorios";
      this.alertClassIns = 'alert-warning';
    }
  }

  CreateStud() {
    this.messageReg = '';
    this.alertClassReg = '';
 
    let firstName = (document.getElementById('firstName') as HTMLSelectElement).value;
    let lastName = (document.getElementById('lastName') as HTMLSelectElement).value;
    let identificationReg = (document.getElementById('identificationReg') as HTMLSelectElement).value;
    if (firstName != "" && lastName != "" && identificationReg != "") {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json', 
      });
      this.httpCliente.post('https://localhost:7285/api/interrapidisimo/createStudent/' + firstName +
        '/' + lastName + '/' + identificationReg, { headers })
        .subscribe((result: any) => {

          if (result.errorStatus) {
            this.messageReg = result.message;
            this.alertClassReg = 'alert-warning';
          } else {
            (document.getElementById('firstName') as HTMLSelectElement).value = '';
            (document.getElementById('lastName') as HTMLSelectElement).value = '';
            (document.getElementById('identificationReg') as HTMLSelectElement).value = '';
            this.messageReg = result.message;
            this.alertClassReg = 'alert-success';
          }

        });
    }
    else {
      this.messageReg = "Todos los campos son obligatorios";
      this.alertClassReg = 'alert-warning';
    }

  }

  onProfessorChange(event: Event) {
    let professor = (document.getElementById('professor') as HTMLSelectElement).value;
    if (professor == '') {
      this.subOptions = [
      ];
    } else {
      const identificacion = event.target as HTMLInputElement;
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
      });
      this.httpCliente.get('https://localhost:7285/api/interrapidisimo/getSubject/' + identificacion.value, { headers })
        .subscribe((result: any) => {
          const dynamicOptions = JSON.parse(result.message);

          this.subOptions = [
            { Value: '', Text: 'Seleccione una materia' },
            ...dynamicOptions
          ];

        });
    }
  }

  onIdentificationChange(event: Event) {
    const identificacion
      = event.target as HTMLInputElement;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 
    });
    if (identificacion.value == '') {
      this.options = [

      ];
    }
    else {
      this.httpCliente.get('https://localhost:7285/api/interrapidisimo/getClass/' + identificacion.value, { headers })
        .subscribe((result: any) => {
          const dynamicOptions = JSON.parse(result.message);

          if (dynamicOptions.length > 0) {
            this.options = [
              { Value: '', Text: 'Seleccione una materia' }, 
              ...dynamicOptions
            ];
            console.log(this.options)
            this.message = '';
            this.alertClass = '';
          } else {
            this.message = 'El estudiante no se encuentra en iscrito a ninguna clase';
            this.alertClass = 'alert-warning';
          }

        });
    }
  }

  searchSubjectStudent() {
    this.message = "";
    this.alertClass = "";
    const idClass = (document.getElementById('subject') as HTMLSelectElement).value.replace("subject", "");
    let identificacion = (document.getElementById('identificationVer') as HTMLSelectElement).value;
    if (idClass == "" && identificacion == "") {
      this.message = "Todos los campos son obligatorios";
      this.alertClass = "alert-warning";
    }
    else {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
      });
      this.httpCliente.get('https://localhost:7285/api/interrapidisimo/getClassAll/' + idClass, { headers })
        .subscribe((result: any) => {
          this.students = JSON.parse(result.message);

          this.httpCliente.get('https://localhost:7285/api/interrapidisimo/getClass/' + identificacion, { headers })
            .subscribe((result: any) => {
              this.professors = JSON.parse(result.message);
            });
        });
    }
  }

}
