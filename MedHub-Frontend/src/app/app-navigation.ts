import {TokenService} from "./shared/services/token.service";

export const doctorNavigation = [
  {
    text: 'Home',
    path: '/home',
    icon: 'home'
  },
  {
    text: 'Patient List',
    path: '/pages/patient-list',
    icon: 'folder'
  },
  {
    text: 'Test Type List',
    path: '/pages/test-type-list',
    icon: 'folder'
  },
  {
    text: 'Test Request Create',
    path: '/pages/test-request-create',
    icon: 'folder'
  }
];

const tokenService = new TokenService();

let userId;
if (tokenService.isTokenValid()) {
  userId = tokenService.getUserId();
}


export const patientNavigation = [
  {
    text: 'Patient Tests',
    path: `/pages/patient-tests/${userId}`,
    icon: 'folder'
  }
];

export const adminNavigation = [
  {
    text: 'Home',
    path: '/home',
    icon: 'home'
  },
  {
    text: 'Patient List',
    path: '/pages/patient-list',
    icon: 'folder'
  },
  {
    text: 'Test Type List',
    path: '/pages/test-type-list',
    icon: 'folder'
  },
  {
    text: 'Test Request Create',
    path: '/pages/test-request-create',
    icon: 'folder'
  },
  {
    text: 'Clinic List',
    path: '/pages/clinic-list',
    icon: 'folder'
  },
  {
    text: 'Doctor List',
    path: '/pages/doctor-list',
    icon: 'folder'
  }
];

