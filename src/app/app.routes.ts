import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Layout } from './pages/layout/layout';
import { AppointmentList } from './pages/appointment-list/appointment-list';
import { NewAppointment } from './pages/new-appointment/new-appointment';
import { PatientList } from './pages/patient-list/patient-list';

export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: Login },
    {
        path: '', component: Layout,
        children: [
            { path: 'appointment-list', component: AppointmentList },
            { path: 'new-appointment', component: NewAppointment },
            { path: 'patient-list', component: PatientList },
        ]
    }
];
