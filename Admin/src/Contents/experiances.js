import React, { useEffect, useState } from 'react';
import { getData } from '../Services/apiService';

import {
    CCard,
    CCardBody,
    CCardHeader,
    CCol,
    CRow,
    CTable,
    CTableBody,
    CTableHead,
    CTableHeaderCell,
    CTableRow,
    CTableDataCell,
  } from '@coreui/react'
  import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
  import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons'
  import './About.css'
  import '@coreui/coreui/dist/css/coreui.min.css'
  import Button from 'react-bootstrap/Button'
  import Form from 'react-bootstrap/Form'
  import Modal from 'react-bootstrap/Modal'
  import { getEducation,deleteEducation, addEducation,updateEducation } from '../Services/apiService';

const Experiances = () => {
     const [data, setData] = useState([])
      const [isEditing, setIsEditing] = useState(false)
      const [currentItem, setCurrentItem] = useState({
        id: null,
        schoolName: '',
        title: '',
        fromDate: Date,
        toDate: Date,
        description: '',
        status: '',
        isActive: true,
      })
    
      useEffect(() => {
        getData()
      }, [])
      const [show, setShow] = useState(false)
    
      const handleClose = () => setShow(false)
      const getData = () => {
        getEducation('/Experiance/GetAllExperiance').then((response) => setData(response.data.data))
        .catch((error) => console.error(error));
      }
      const handleAddOrUpdate = () => {
        const formattedItem = {
          ...currentItem,
          
          id: isEditing ? currentItem.id : null, 
        };
      
        const url = '/Experiance/AddExperiance'
        const url1 ='/Experiance/UpdateExperiance'
        if(isEditing)
            updateEducation(url1,formattedItem).then((res)=>{
              if(res.data.isSuccess){
                getData();
                resetForm();
                handleClose();
                console.log(res.data.message)
              }else{
            console.log(res.data.message)
              }
            })
       else{
              addEducation(url,formattedItem).then((res)=>{
                if(res.data.isSuccess){
                  console.log(res.data.message);
                  getData();
                  resetForm();
                  handleClose();
                }
                else{
                  console.log(res.data.message)
                }
              })
    
        }
        
      };
    
      
      const handleEdit = (item) => {
        setCurrentItem(item)
        setIsEditing(true)
        setShow(true)
      }
    
      const handleDelete = (id) => {
          deleteEducation(`/Experiance/DeleteExperinace?Id=${id}`).then((res)=>{
            if(res.data.isSuccess){
           console.log(res.data.message);
           getData();
            }
            else{
              console.log(res.data.message)
            }
          })
    
      }
    
      const handleAdd = () => {
        resetForm()
        setIsEditing(false)
        setShow(true)
      }
      const formatDate = (date) => {
        if (!date) return '';
        return new Intl.DateTimeFormat('en-GB', {
          day: '2-digit',
          month: '2-digit',
          year: 'numeric',
        }).format(new Date(date));
      };
    
      const resetForm = () => {
        setCurrentItem({
          id: '',
          schoolName: '',
          title: '',
          fromDate: '',
          toDate: '',
          description: '',
          status: '',
          isActive: true,
        })
        setIsEditing(false)
      }

    return (
        <>
      <CRow>
        <CCol xs={12}>
          <CCard className="mb-4 shadow-sm">
            <CCardHeader className="bg-primary text-white">
              <strong>Experiance</strong>
            </CCardHeader>
            <CCardBody>
              <div className="d-flex justify-content-end mb-2">
                <button className="btn btn-success" onClick={handleAdd}>
                  Add New
                </button>
              </div>
              <CTable responsive>
                <CTableHead color="light">
                  <CTableRow>
                    <CTableHeaderCell>#</CTableHeaderCell>
                    <CTableHeaderCell>School Name</CTableHeaderCell>
                    <CTableHeaderCell>Title</CTableHeaderCell>
                    <CTableHeaderCell>From Date</CTableHeaderCell>
                    <CTableHeaderCell>To Date</CTableHeaderCell>
                    <CTableHeaderCell>Description</CTableHeaderCell>
                    <CTableHeaderCell>Status</CTableHeaderCell>
                    <CTableHeaderCell>Action</CTableHeaderCell>
                  </CTableRow>
                </CTableHead>
                <CTableBody>
                  {data && data.length
                    ? data.map((item, index) => (
                        <CTableRow key={index}>
                          <CTableHeaderCell scope="row">{index + 1}</CTableHeaderCell>
                          <CTableDataCell>{item.schoolName}</CTableDataCell>
                          <CTableDataCell>{item.title}</CTableDataCell>
                          <CTableDataCell>{formatDate(item.fromDate)}</CTableDataCell>
                          <CTableDataCell>{formatDate(item.toDate)}</CTableDataCell>
                          <CTableDataCell>{item.description}</CTableDataCell>
                          <CTableDataCell>{item.status}</CTableDataCell>
                          <CTableDataCell>
                            <div className="d-flex gap-1">
                              <button className="btn btn-sm" onClick={() => handleEdit(item)}>
                                <FontAwesomeIcon icon={faEdit} />
                              </button>
                              <button
                                className="btn btn-sm"
                                onClick={() => handleDelete(item.id)}
                              >
                                <FontAwesomeIcon icon={faTrash} />
                              </button>
                            </div>
                          </CTableDataCell>
                        </CTableRow>
                      ))
                    : 'Loading'}
                </CTableBody>
              </CTable>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>

      {/* Modal for Add/Edit */}

      <Modal show={show} onHide={handleClose} size="lg">
        <Modal.Header closeButton>
          <Modal.Title> {isEditing ? 'Update' : 'Add'} Education </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <div className="row">
              <div className="col-lg-6">
                <Form.Group controlId="schoolName">
                  <Form.Label>School Name</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.schoolName}
                    onChange={(e) => setCurrentItem({ ...currentItem, schoolName: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="grade">
                  <Form.Label>Jobe Title</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.title}
                    onChange={(e) => setCurrentItem({ ...currentItem, title: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="fromDate">
                  <Form.Label>From Date</Form.Label>
                  <Form.Control
                    type="date"
                    value={currentItem.fromDate}
                    onChange={(e) => setCurrentItem({ ...currentItem, fromDate: e.target.value })}
                  />
                </Form.Group>
              </div>
              <div className="col-lg-6">
                <Form.Group controlId="toDate">
                  <Form.Label>To Date</Form.Label>
                  <Form.Control
                    type="date"
                    value={currentItem.toDate}
                    onChange={(e) => setCurrentItem({ ...currentItem, toDate: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="description">
                  <Form.Label>Description</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.description}
                    onChange={(e) =>
                      setCurrentItem({ ...currentItem, description: e.target.value })
                    }
                  />
                </Form.Group>
               
               
              </div>
            </div>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => handleClose(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleAddOrUpdate}>
            {isEditing ? 'Update' : 'Add'}
          </Button>
        </Modal.Footer>
      </Modal>
    </>
    );
};

export default Experiances;
