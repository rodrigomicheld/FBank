'use client';
import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import styles from './modalMessage.module.css';

export const ModalMessage = (props) =>{
  const [show, setShow] = useState(false);
  const handleClose = () => {
      setShow(false);
      if(props?.props?.fnCallback)
        props?.props?.fnCallback();
  }
  const handleShow = () => setShow(true);
  const [modalTitle, setModalTitle] = useState("exemplo titulo");
  const [modalBody, setModalBody] = useState("exemplo text body");
  
  useEffect(()=>{
    setShow(props.props.show);
    setModalTitle(props.props.textTitle);
    setModalBody(props.props.textBody);
  },[props])
  return (
    <>
      <Modal show={show} onHide={handleClose} className={styles.modal}>
        <Modal.Header closeButton>
          <Modal.Title> {modalTitle}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{modalBody}</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}