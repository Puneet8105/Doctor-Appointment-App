import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AppointmentList } from '../appointment-list/appointment-list';

@Component({
  selector: 'app-login',
  standalone:true,
  imports: [FormsModule,CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class Login {

  userObj :any = {
    username:'',
    password:''
  }
  router = inject(Router);
  onLogin() {
    if(this.userObj.username == 'admin' && this.userObj.password == 1234){
      this.router.navigateByUrl("/appointment-list")
    }
    else{
      alert("Wrong credentials")
    }
  }

}
