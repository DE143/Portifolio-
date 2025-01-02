import React, { useEffect, useState } from 'react'
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
import axios from 'axios'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons'
import './About.css'
import '@coreui/coreui/dist/css/coreui.min.css'
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'
import Modal from 'react-bootstrap/Modal'
import { deleteData } from '../Services/apiService'
const About = () => {
  const [data, setData] = useState([])
  const [modalVisible, setModalVisible] = useState(false)
  const [isEditing, setIsEditing] = useState(false)
  const [currentItem, setCurrentItem] = useState({
    id: '',
    fullName: '',
    title: '',
    description: '',
    photo: null,
    degree: '',
    phoneNumber: '',
    email: '',
    address: '',
    birthDay: '',
    experience: '',
    freelance: '',
    status: '',
    isActive: false,
  })

  useEffect(() => {
    getData()
    console.log('modal', modalVisible)
  }, [])
  const [show, setShow] = useState(false)

  const handleClose = () => setShow(false)
  const getData = () => {
    axios
      .get('http://localhost:5015/api/AboutUs/GetAllAboutUsData')
      .then((res) => {
        setData(res.data.data)
      })
      .catch((error) => console.log(error))
  }

  const handleAddOrUpdate = () => {
    const formData = new FormData()
    formData.append('FullName', currentItem.fullName)
    formData.append('Title', currentItem.title)
    formData.append('Description', currentItem.description)
    formData.append('Photo', currentItem.photo)
    formData.append('Degree', currentItem.degree)
    formData.append('PhoneNumber', currentItem.phoneNumber)
    formData.append('Email', currentItem.email)
    formData.append('Address', currentItem.address)
    formData.append('BirthDay', currentItem.birthDay)
    formData.append('Experiance', currentItem.experience)
    formData.append('Freelance', currentItem.freelance)
    formData.append('Id', currentItem.id)
    if (isEditing) {
      axios
        .put(`http://localhost:5015/api/AboutUs/Update/${currentItem.id}`, formData)
        .then(() => {
          getData()
          resetForm()
        })
        .catch((error) => console.log(error))
    } else {
      // Add logic
      axios
        .post('http://localhost:5015/api/AboutUs/AddAboutUs', formData)
        .then(() => {
          getData()
          resetForm()
        })
        .catch((error) => console.log(error))
    }
    setModalVisible(false)
  }
  const handleEdit = (item) => {
    console.log('seny', item)
    setCurrentItem(item)
    setIsEditing(true)
    setShow(true)
  }

  const handleDelete = (id) => {
    deleteData(`/AboutUs/DeleteAboutUs?id=${id}`).then((res) => {
      if (res.data.isSuccess) {
        getData()
      } else {
        console.log(res.data.message)
      }
    })
  }

  const handleAdd = () => {
    resetForm()
    setIsEditing(false)
    setShow(true)
  }

  const resetForm = () => {
    setCurrentItem({
      id: '',
      fullName: '',
      title: '',
      description: '',
      photo: '',
      degree: '',
      phoneNumber: '',
      email: '',
      address: '',
      birthDay: '',
      experience: '',
      freelance: '',
      status: '',
      isActive: false,
    })
    setIsEditing(false)
  }

  return (
    <>
      <CRow>
        <CCol xs={12}>
          <CCard className="mb-4 shadow-sm">
            <CCardHeader className="bg-primary text-white">
              <strong>About Us</strong>
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
                    <CTableHeaderCell>Photo</CTableHeaderCell>
                    <CTableHeaderCell>Full Name</CTableHeaderCell>
                    <CTableHeaderCell>Title</CTableHeaderCell>
                    <CTableHeaderCell>Description</CTableHeaderCell>
                    <CTableHeaderCell>Education Level</CTableHeaderCell>
                    <CTableHeaderCell>Phone Number</CTableHeaderCell>
                    <CTableHeaderCell>Email</CTableHeaderCell>
                    <CTableHeaderCell>Address</CTableHeaderCell>
                    <CTableHeaderCell>Birth Day</CTableHeaderCell>
                    <CTableHeaderCell>Experience</CTableHeaderCell>
                    <CTableHeaderCell>Freelance</CTableHeaderCell>
                    <CTableHeaderCell>Status</CTableHeaderCell>
                    <CTableHeaderCell>Action</CTableHeaderCell>
                  </CTableRow>
                </CTableHead>
                <CTableBody>
                  {data && data.length
                    ? data.map((item, index) => (
                        <CTableRow key={index}>
                          <CTableHeaderCell scope="row">{index + 1}</CTableHeaderCell>
                          <CTableDataCell>
                            {' '}
                            {item.photoUrl && (
                              <img
                                src={`http://localhost:5015/${item.photoUrl}`}
                                alt="About Image"
                                style={{ width: '300px', height: '200px', objectFit: 'cover' }}
                              />
                            )}
                          </CTableDataCell>
                          <CTableDataCell>{item.fullName}</CTableDataCell>
                          <CTableDataCell>{item.title}</CTableDataCell>
                          <CTableDataCell>{item.description}</CTableDataCell>
                          <CTableDataCell>{item.degree}</CTableDataCell>
                          <CTableDataCell>{item.phoneNumber}</CTableDataCell>
                          <CTableDataCell>{item.email}</CTableDataCell>
                          <CTableDataCell>{item.address}</CTableDataCell>
                          <CTableDataCell>{item.birthDay}</CTableDataCell>
                          <CTableDataCell>{item.experience}</CTableDataCell>
                          <CTableDataCell>{item.freelance}</CTableDataCell>
                          <CTableDataCell>{item.status}</CTableDataCell>
                          <CTableDataCell>
                            <div className="d-flex gap-1">
                              <button className="btn btn-info" onClick={() => handleEdit(item)}>
                                <FontAwesomeIcon icon={faEdit} />
                              </button>
                              <button
                                className="btn btn-danger"
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
          <Modal.Title> {isEditing ? 'Update' : 'Add'} About Us </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <div className="row">
              <div className="col-lg-6">
                <Form.Group controlId="fullName">
                  <Form.Label>Full Name</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.fullName}
                    onChange={(e) => setCurrentItem({ ...currentItem, fullName: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="title">
                  <Form.Label>Title</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.title}
                    onChange={(e) => setCurrentItem({ ...currentItem, title: e.target.value })}
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
                <Form.Group controlId="photo">
                  <Form.Label>Photo</Form.Label>
                  <Form.Control
                    type="file"
                    value={currentItem.photo}
                    onChange={(e) => setCurrentItem({ ...currentItem, photo: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="degree">
                  <Form.Label>Education Level</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.degree}
                    onChange={(e) => setCurrentItem({ ...currentItem, degree: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="phoneNumber">
                  <Form.Label>Phone Number</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.phoneNumber}
                    onChange={(e) =>
                      setCurrentItem({ ...currentItem, phoneNumber: e.target.value })
                    }
                  />
                </Form.Group>
              </div>
              <div className="col-lg-6">
                <Form.Group controlId="email">
                  <Form.Label>Email</Form.Label>
                  <Form.Control
                    type="email"
                    value={currentItem.email}
                    onChange={(e) => setCurrentItem({ ...currentItem, email: e.target.value })}
                  />
                </Form.Group>

                <Form.Group controlId="address">
                  <Form.Label>Address</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.address}
                    onChange={(e) => setCurrentItem({ ...currentItem, address: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="birthDay">
                  <Form.Label>Birth Day</Form.Label>
                  <Form.Control
                    type="date"
                    value={currentItem.birthDay}
                    onChange={(e) => setCurrentItem({ ...currentItem, birthDay: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="experience">
                  <Form.Label>Experience</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.experience}
                    onChange={(e) => setCurrentItem({ ...currentItem, experience: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="freelance">
                  <Form.Label>Freelance</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.freelance}
                    onChange={(e) => setCurrentItem({ ...currentItem, freelance: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="status">
                  <Form.Label>Status</Form.Label>
                  <Form.Control
                    type="text"
                    value={currentItem.status}
                    onChange={(e) => setCurrentItem({ ...currentItem, status: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="isActive">
                  <Form.Check
                    type="checkbox"
                    label="Is Active"
                    checked={currentItem.isActive}
                    onChange={(e) => setCurrentItem({ ...currentItem, isActive: e.target.checked })}
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
  )
}

export default About
