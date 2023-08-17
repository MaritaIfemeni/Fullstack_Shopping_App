export interface User {
    id: string;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    address: string;
    city: string;
    postcode: string;
    phone: string;
    avatar: string;
    password: string;
    UserRole: "customer" | "admin";
    isAdmin: boolean;
}
