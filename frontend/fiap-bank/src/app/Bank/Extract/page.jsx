'use client';
import useExtract from '@/Hooks/useExtract';
import Table from 'react-bootstrap/Table';
import styles from './extract.module.css';
export default function Extract() {   
    const { userExtract } = useExtract();
    return(<div className={styles.extractComponent}>
                <Table striped bordered hover size="lg">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Data Transação</th>
                            <th>Descrição</th>
                            <th>Valor Transação</th>
                        </tr>
                    </thead>
                    <tbody>
                        {userExtract && userExtract.length>0 &&
                                userExtract.map((item,i)=>{
                                    return(
                                        <tr>
                                            <td></td>
                                            <td>{item.dateTransaction}</td>
                                            <td>{item.description}</td>
                                            <td>{item.amount}</td>
                                        </tr>)
                                })
                             }
                       
                    </tbody>
                </Table>
        </div>
    )
}