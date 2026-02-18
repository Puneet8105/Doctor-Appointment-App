import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Master } from '../../service/master';


@Component({
  selector: 'app-new-appointment',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './new-appointment.html',
  styleUrl: './new-appointment.css'
})
export class NewAppointment {



  masterServ = inject(Master);

  // Date limits
  today = new Date().toISOString().split('T')[0];
  maxDate = new Date(Date.now() + 7 * 24 * 60 * 60 * 1000)
    .toISOString().split('T')[0];

  apontObj = {
    patientName: "",
    mobileNo: "",
    city: "",
    email: "",
    address: "",
    appointmentDate: ""
  };

  OnSave(form: any) {
    const payload = {
      ...this.apontObj,
      appointmentDate: new Date(this.apontObj.appointmentDate).toISOString()
    };

    this.masterServ.createNewAppointment(payload).subscribe({
      next: (res: any) => {

        alert(res?.message || "Appointment Created Successfully");
        form.resetForm();
      },
      error: (err) => {
        console.error(err);

       
         const msg = err.error?.message || "Something went wrong";
      alert(msg);
         if (msg.includes("7 days")) {
        this.apontObj.appointmentDate = "";  
        
      }
      }
    });
  }
}
