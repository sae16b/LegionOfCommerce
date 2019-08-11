export interface AuthResult {
  token: string;
  refreshToken: string;
  errors?: string[];
  success: boolean;
}
