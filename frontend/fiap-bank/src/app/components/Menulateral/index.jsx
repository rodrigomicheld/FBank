import Image from "next/image"
import styles from './menulateral.module.css';
import { ItemMenu } from "../ItemMenu";



export const Menulateral = () =>{
    return(<div className={styles.Menulateral}>       
       
        <ul className={styles.ItensMenu}>
            <ItemMenu props={ {MenuName:"Extrato", LogoSrc:'/fiapbanklogo.png' , src:"../Bank/Extract" ,classIcon:"bi bi-card-list" }} />
        </ul>
        
        <ul className={styles.ItensMenu}>
            <ItemMenu props={ {MenuName:"Operação", LogoSrc:'/fiapbanklogo.png' , src:"../Bank/Transaction" ,classIcon:"bi bi-card-list" }} />
        </ul>
        {/* <ul className={styles.ItensMenu}>
            <ItemMenu props={ {MenuName:"Clientes", LogoSrc:'/fiapbanklogo.png', src:"../Bank/Clientes" , classIcon:"bi-card-list"}} />
        </ul>
        <ul className={styles.ItensMenu}>            
            <ItemMenu props={ {MenuName:"Sair", LogoSrc:'/fiapbanklogo.png', src:"../Login", classIcon:"bi bi-box-arrow-in-left" }}  />
        </ul> */}
    </div>)
}