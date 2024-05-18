'use client';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState, useEffect } from 'react';
import useUtils from '@/Hooks/useUtils';
import Image from "next/image";
import styles from "./page.module.css";
import Login from './Login/page';
import Bank from './Bank/page';




export default function Home() {
  const { isNullOrEmpty, getCookie } = useUtils();
  const [isAuthenticated,setIsAuthenticated] = useState(false);
  useEffect(()=>{
    let cookie = getCookie("fiapBankCookie");
    setIsAuthenticated(!isNullOrEmpty(cookie));
  },[]);
  
 
 
  
  if(!isAuthenticated){
    return (
      <main>      
        <Login />
      </main>
    );
  }else{
    return (
      <main>      
        <Bank />
      </main>
    );
  }
}
