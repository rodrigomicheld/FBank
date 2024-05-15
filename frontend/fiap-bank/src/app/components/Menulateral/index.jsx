import Image from "next/image"
import styles from './menulateral.module.css';
import { ItemMenu } from "../ItemMenu";



export const Menulateral = () =>{
    return(<div className={styles.Menulateral}>       
        <div>
            <Image 
                src={'/fiapbanklogo.png'}
                alt="Home"
                height={80}
                width={120}
            />
        </div>
        <ul className={styles.ItensMenu}>
            <ItemMenu props={ {MenuName:"Operacao", LogoSrc:'/fiapbanklogo.png' , src:"../Bank/Operacao" ,classIcon:"bi bi-currency-dollar" }} />
        </ul>
        <ul className={styles.ItensMenu}>
            <ItemMenu props={ {MenuName:"Clientes", LogoSrc:'/fiapbanklogo.png', src:"../Bank/Clientes" , classIcon:"bi-card-list"}} />
        </ul>
        <ul className={styles.ItensMenu}>            
            <ItemMenu props={ {MenuName:"Sair", LogoSrc:'/fiapbanklogo.png', src:"../Login", classIcon:"bi bi-box-arrow-in-left" }}  />
        </ul>
    </div>)
}