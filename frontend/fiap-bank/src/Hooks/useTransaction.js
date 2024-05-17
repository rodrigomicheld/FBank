'use client';

import { useState } from "react";
import useClienteApiService from "./useClienteApiService";


const useTransaction = ()=>{
    const [modalData, setModalData ]    = useState({});   
    const { registerTransaction} = useClienteApiService(); 
    const initalFormData = {
        Tipo:0,
        Valor:0
    };
    const [formData, setFormData ]    = useState(initalFormData); 
    const handleChange = (e) => {
        e.stopPropagation();
        e.preventDefault();
        const { name, value } = e.target;
        let valor = value;
        if(name == 'Valor'){
            valor = value.replace(/[^0-9\.]+/g, '');
            setFormData(prevState => ({
                ...prevState,
                [name]: valor,
            }));
        }else{
            setFormData(prevState => ({
                ...prevState,
                [name]: value,
            }));
        }
        
    };
    
    const registrarTransacao = (data)=>{
        console.log(data);
        const request = {
            "value": data.Valor
        }
         
        registerTransaction(data.Tipo, request, async(response)=>{
            if(response.ok){
                //const result = await response.json();
                console.log("Success:", "Transação realizada com sucesso");                
                setModalData({
                    textTitle:"Transação",
                    textBody:"Transação realizada com sucesso", 
                    show:true,
                    fnCallback:()=>{
                            setModalData({
                                textTitle:"",
                                textBody:"", 
                            show:false});
                        }
                    });    
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

    return {formData,modalData,handleChange,registrarTransacao}
}
export default useTransaction;