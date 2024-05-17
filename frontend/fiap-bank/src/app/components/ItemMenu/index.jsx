'use client';
import Image from "next/image"
import styles from "./itemmenu.module.css";
import useUtils from "@/Hooks/useUtils";
import { useRouter } from "next/navigation";


export const ItemMenu = ({props})=>{
    const router = useRouter();
    const { deleteCookie } = useUtils();
    const redirecionar = ()=>{
        if(props.src == "../Login"){
            deleteCookie("fiapBankCookie");
        }
        router.push(props.src);
    }
    return(
        <li className={styles.ItemMenu}>
            <div>
                <i class={props.classIcon}></i>
                 <button onClick={()=>{redirecionar()}}>                    
                    {props.MenuName}
                </button>
            </div>
        </li>   
    ) 
}