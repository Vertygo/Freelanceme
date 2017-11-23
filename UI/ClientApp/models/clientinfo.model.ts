import { Project } from './project.model';

export interface ClientInfo {
    Id: string
    Name: string;
    Address: string;
    IsCompany: boolean;
    IsActive: boolean;
    Projects: Project[];
}