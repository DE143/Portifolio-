import React, { useEffect, useState } from 'react';
import { getData, deleteData, getEducation, updateData } from '../Services/apiService';
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
  import {  faTrash } from '@fortawesome/free-solid-svg-icons'
  import './About.css'

const ContactUs = () => {
    const [data, setData] = useState([]);
    useEffect(() => {
        getContactData();          
    }, []);
    const getContactData=()=>{
       getData('/ContactUs/GetAllContactUs').then((res) => setData(res.data.data))
       .catch((error) => console.error(error));
     
     }
   const   handleDelete=(id)=>{
        updateData(`/ContactUs/DeleteContactUs?Id=${id}`).then((res)=>{
            if(res.data.isSuccess){
            console.log(res.data.isSuccess)
                getContactData();
            }else{
                console.log(res.data.message)
            }
        })
     }

    return (
        <CRow>
                <CCol xs={12}>
                  <CCard className="mb-4 shadow-sm">
                    <CCardHeader className="bg-primary text-white">
                      <strong>Contact Us Page</strong>
                    </CCardHeader>
                    <CCardBody>
                     
                      <CTable responsive>
                        <CTableHead color="light">
                          <CTableRow>
                            <CTableHeaderCell>#</CTableHeaderCell>
                            <CTableHeaderCell>Full Name</CTableHeaderCell>
                            <CTableHeaderCell>Email</CTableHeaderCell>
                            <CTableHeaderCell>Subject</CTableHeaderCell>
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
                                  <CTableDataCell>{item.fullName}</CTableDataCell>
                                  <CTableDataCell>{item.email}</CTableDataCell> 
                                  <CTableDataCell>{item.subject}</CTableDataCell>
                                  <CTableDataCell>{item.description}</CTableDataCell>
                                  <CTableDataCell>{item.isActive ? 'Active' : 'InActive'}</CTableDataCell>
                                  <CTableDataCell>
                                    <div className="d-flex gap-1">
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
                            : 'No Data'}
                        </CTableBody>
                      </CTable>
                    </CCardBody>
                  </CCard>
                </CCol>
              </CRow>
    );
};

export default ContactUs;
