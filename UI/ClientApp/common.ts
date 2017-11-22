export interface ClientInfo {
    Id: string
    Name: string;
    Address: string;
    IsCompany: boolean;
    IsActive: boolean;
    Projects: Project[];
}

export interface Project {
    Id: string,
    Name: string
}