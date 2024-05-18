'use client';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';

import { useEffect, useState } from 'react';
import { ModalMessage } from '@/app/components/ModalMessage';
import useTransaction from '@/Hooks/useTransaction';

export default function Transaction (){

    const { formData, modalData, handleChange,registrarTransacao } = useTransaction();
   
    return(<div >
        <ModalMessage props={modalData}  />
        <h1>Realizar Operação</h1>
        <br/>
        <br/>
        <Row>
            <Col md={{ span: 6, offset: 3 }}>
                <Form>
                    <Row >
                        <Form.Label>Tipo</Form.Label>                       
                        <Form.Select aria-label="Default select example"
                             name='Tipo'
                             value={formData.Tipo}
                             onChange={handleChange}
                            >
                            <option value="0">Selecione o Tipo</option>
                            <option value="1">Depósito</option>
                            <option value="2">Saque</option>
                            <option value="3">Transferência</option>
                        </Form.Select>
                        
                    </Row>
                   
                    <Row >
                        <Form.Label>Valor</Form.Label>
                        <Form.Control type="text" placeholder="Insira o  valor"
                         name='Valor'
                         value={formData.Valor}
                         onChange={handleChange} />
                    </Row>                   
                    <Row >
                        <Col md={{ span: 4, offset: 8 }}> 
                            <Button variant="primary" size="ls"onClick={(e)=>{registrarTransacao(formData);}}>
                                Salvar                    
                            </Button>
                        </Col>
                    </Row>
                </Form>
            </Col>
        </Row>
    </div>)
}