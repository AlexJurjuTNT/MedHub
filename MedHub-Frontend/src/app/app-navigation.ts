import {TokenService} from "./shared/services/token.service";

export const getNavigation = (tokenService: TokenService) => {
  const userId = tokenService.isTokenValid() ? tokenService.getUserId() : null;
  const userRole = tokenService.isTokenValid() ? tokenService.getUserRole() : null;

  const doctorNavigation = [
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
    }
  ];

  const patientNavigation = [
    {
      text: 'Patient Tests',
      path: `/pages/patient-tests/${userId}`,
      icon: 'folder'
    }
  ];

  const adminNavigation = [
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

  switch (userRole) {
    case "Admin":
      return adminNavigation;
    case "Doctor":
      return doctorNavigation;
    case "Patient":
      return patientNavigation;
    default:
      return [];
  }
};
