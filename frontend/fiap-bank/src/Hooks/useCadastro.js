'use client';

import { useState } from "react";
import { useRouter } from "next/navigation";
import useUtils from "./useUtils";
import useClienteApiService from "./useClienteApiService";


const useCadastro = ()=>{
    
    const router = useRouter();
    const { isNullOrEmpty, setCookie } = useUtils();
    const { registerUser} = useClienteApiService();
    const [ modalData, setModalData ] = useState({});
    const initalFormData = {
        Nome: '',   
        Email:''     ,
        Cpf: '',
        Password: '',        
    };
    const [formData, setFormData] = useState(initalFormData);
    
    const handleChange = (e) => {
        e.stopPropagation();
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value,
        }));
    };
  
    
    const registrarCadastro = (data)=>{    
        console.log(data);
        const request = {
            "name": data.Nome,
            "document":  data.Cpf,
            "password": data.Password
          }
        registerUser(request, async(response)=>{
            if(response.ok){
                //const result = await response.json();
                console.log("Success:", "Usuário cadastrado com sucesso");
                const fnCallback = ()=>{ router.push("/Login");}
                setModalData({
                    textTitle:"Cadastro Realizado com sucesso",
                    textBody:"Usuário cadastrado com sucesso", 
                    show:true,
                    fnCallback:fnCallback});    
                setFormData(initalFormData);            
            }else{
                return response.text().then((text) =>{    
                    console.log("Error:", text);                 
                    setModalData({
                        textTitle:"Erro",
                        textBody:text, 
                        show:true});

                });                
            }
        });
        

    }

    return {formData,modalData,handleChange,registrarCadastro}
}
export default useCadastro;