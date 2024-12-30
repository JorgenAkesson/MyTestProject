import { Component, OnInit } from '@angular/core';
import { PatientService} from '../Services/patient-service.service';

interface Patient {
  Id: number;
  PatientName: string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  title = 'MyHomePage';
  patients: Patient[] = [];
  displayedColumns: string[] = ['Id', 'PatientName'];
 
  constructor(private patientService: PatientService){}

  ngOnInit(): void {
    this.patients = DATA;
    //this.createPatient();
    //this.fetchPatients();
  }

  fetchPatients() {
    this.patientService.fetchPatients()
        .subscribe(
            (response) => {
                this.patients = response;
            },
            (error) => {
                console.error(error);
            }
        );
  }

  createPatient() {
    this.patientService.createPatient(DATA[0])
        .subscribe(
            (response) => {
                console.log('User created:', response);
                // Refresh the user list after creating a new user
                this.fetchPatients();
              },
            (error) => {
                console.error(error);
            }
        );
  }
}

const DATA: Patient[] = [
  {Id: 1, PatientName: 'Jörgen Åkesson'},
  {Id: 2, PatientName: 'Ingrid Åkesson'},
  {Id: 3, PatientName: 'Alexander Åkesson'},
  {Id: 4, PatientName: 'Ella Åkesson'},
];
