'use client';
import 'bootstrap/dist/css/bootstrap.min.css';
import "./globals.css";
import Login from './Login/page';

const metadata = {
  title: "Fiap Bank",
  description: "Fiap Bank Project",
};

export default function RootLayout({ children }) {
  
    return (
      <html lang="pt">      
        <body>                    
            {children}
        </body>
      </html>
    );  
}
