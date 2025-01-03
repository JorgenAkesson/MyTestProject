import { Component, OnInit } from '@angular/core';
import { PatientService} from '../Services/patient-service.service';
import { BillingsService } from '../Services/billing-service.service';

interface Patient {
  Id: number;
  PatientName: string;
}

interface Billing {
  Id: number;
  PatientName: string;
  // price: number;
  // quantity: number;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  title = 'MyHomePage';
  patients: Patient[] = [];
  billings: Billing[] = [];
  displayedColumns: string[] = ['Id', 'PatientName'];
  displayedBillingsColumns: string[] = ['Id', 'PatientName'];
 
  constructor(private patientService: PatientService, private BillingsService: BillingsService){}

  ngOnInit(): void {
    //this.patients = DATA;
    this.fetchBillings();
    //this.createPatient();
    this.fetchPatients();
  }

  fetchPatients() {
    this.patientService.fetchPatients().subscribe({
      next: (data) => {
          this.patients = data;
      },
      error: (error) => {
          console.log(error)
      },
      complete: () => {
          console.log('complete')
      }
    })  
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

  fetchBillings() {
    this.BillingsService.fetchBillings().subscribe({
      next: (data) => {
          this.billings = data;
      },
      error: (error) => {
          console.log(error)
      },
      complete: () => {
          console.log('complete')
      }
    })  
  }
}

const DATA: Patient[] = [
  {Id: 1, PatientName: 'Jörgen Åkesson'},
  {Id: 2, PatientName: 'Ingrid Åkesson'},
  {Id: 3, PatientName: 'Alexander Åkesson'},
  {Id: 4, PatientName: 'Ella Åkesson'},
];
