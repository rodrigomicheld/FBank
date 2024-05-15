'use client';

import { useState } from "react";
import { useRouter } from "next/navigation";
import useUtils from "./useUtils";


const useCadastro = ()=>{
    const router = useRouter();
    const { isNullOrEmpty, setCookie } = useUtils();
    const [formData, setFormData] = useState({
        Nome: '',   
        Email:''     ,
        Cpf: '',
        Password: '',        
    });
    
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value,
        }));
    };
  
    
    const registrarCadastro = (data)=>{    
        console.log(data);    
        //todo: send data to register user
    }

    return {formData,handleChange,registrarCadastro}
}
export default useCadastro;