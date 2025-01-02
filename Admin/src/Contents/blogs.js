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
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons'
import './About.css'
import '@coreui/coreui/dist/css/coreui.min.css'
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'
import Modal from 'react-bootstrap/Modal'
import {
  getEducation,
  deleteEducation,
  addEducation,
  updateEducation,
} from '../Services/apiService'
import axios from 'axios'
const Blogs = () => {
  const [blogs, setBlogs] = useState([])
  const [show, setShow] = useState(false)
  const handleClose = () => setShow(false)
  const [isEditing, setIsEditing] = useState(false)
  useEffect(() => {
    getBlogs()
  }, [])

  const getBlogs = () => {
    const fetchBlogs = async () => {
      try {
        const response = await axios.get('http://localhost:5015/api/Blogs/GetAllBlogs')
        setBlogs(response.data.data)
      } catch (error) {
        console.error('Error fetching blogs:', error)
      }
    }
    fetchBlogs()
  }

  const handleEdit = (item) => {
    setFormData(item)
    setIsEditing(true)
    setShow(true)
  }
  const handleAdd = () => {
    resetForm()
    setIsEditing(false)
    setShow(true)
  }
  const handleDelete = (id) => {
    deleteEducation(`/Blogs/DeleteBlog?Id=${id}`).then((res) => {
      if (res.data.isSuccess) {
        getBlogs()
      } else {
        console.log(res.data.message)
      }
    })
  }
  const resetForm = () => {
    setFormData({
      id: '',
      image: null,
      description: '',
      date: '',
      status: '',
      isActive: true,
    })
    setIsEditing(false)
  }

  const [formData, setFormData] = useState({
    image: null,
    description: '',
    date: '',
    status: '',
    isActive: false,
  })

  const handleInputChange = (e) => {
    const { name, value } = e.target
    setFormData({ ...formData, [name]: value })
  }

  const handleFileChange = (e) => {
    setFormData({ ...formData, image: e.target.files[0] })
  }

  const handleSubmit = async (e) => {
    e.preventDefault()

    // Create FormData
    const data = new FormData()
    data.append('Image', formData.image)
    data.append('Description', formData.description)
    data.append('Date', formData.date)
    data.append('Status', formData.status)
    data.append('IsActive', formData.isActive)

    try {
      const response = await axios.post('http://localhost:5015/api/Blogs/AddBlog', data, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      })
      getBlogs()
      console.log(response.data)
      alert('Blog added successfully!')
    } catch (error) {
      console.error(error)
      alert('Failed to add the blog.')
    }
  }

  return (
    <>
      <CRow>
        <CCol xs={12}>
          <CCard className="mb-4 shadow-sm">
            <CCardHeader className="bg-primary text-white">
              <strong>Blogs And News Page</strong>
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
                    <CTableHeaderCell>Description</CTableHeaderCell>
                    <CTableHeaderCell> Date</CTableHeaderCell>
                    <CTableHeaderCell>Status</CTableHeaderCell>
                    <CTableHeaderCell>Action</CTableHeaderCell>
                  </CTableRow>
                </CTableHead>
                <CTableBody>
                  {blogs && blogs.length
                    ? blogs.map((item, index) => (
                        <CTableRow key={index}>
                          <CTableHeaderCell scope="row">{index + 1}</CTableHeaderCell>
                         
                          <CTableDataCell>
                            {' '}
                            {item.imageUrl && (
                              <img
                                src={`http://localhost:5015/${item.imageUrl}`}
                                alt="Blog"
                                style={{ width: '300px', height: '200px', objectFit: 'cover' }}
                              />
                            )}
                          </CTableDataCell> 
                          <CTableDataCell>{item.description}</CTableDataCell>
                          <CTableDataCell>
                            {new Date(item.date).toLocaleDateString()}
                          </CTableDataCell>
                          <CTableDataCell>{item.status}</CTableDataCell>
                          <CTableDataCell>
                            <div className="d-flex gap-1">
                              <button className="btn btn-sm" onClick={() => handleEdit(item)}>
                                <FontAwesomeIcon icon={faEdit} />
                              </button>
                              <button className="btn btn-sm" onClick={() => handleDelete(item.id)}>
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

      <form onSubmit={handleSubmit}>
        <div>
          <label>Image:</label>
          <input type="file" name="image" onChange={handleFileChange} required />
        </div>
        <div>
          <label>Description:</label>
          <textarea
            name="description"
            value={formData.description}
            onChange={handleInputChange}
            required
          ></textarea>
        </div>
        <div>
          <label>Date:</label>
          <input
            type="date"
            name="date"
            value={formData.date}
            onChange={handleInputChange}
            required
          />
        </div>
        <div>
          <label>Status:</label>
          <select name="status" value={formData.status} onChange={handleInputChange}>
            <option value="Not Approved">Not Approved</option>
            <option value="Approved">Approved</option>
          </select>
        </div>
        <div>
          <label>Is Active:</label>
          <input
            type="checkbox"
            name="isActive"
            checked={formData.isActive}
            onChange={() => setFormData({ ...formData, isActive: !formData.isActive })}
          />
        </div>
        <button type="submit">Add</button>
      </form>

      {/* <Modal show={show} onHide={handleClose} size="lg">
        <Modal.Header closeButton className='bg-info-subtle'>
          <Modal.Title> {isEditing ? 'Update' : 'Add'} Blogs </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <div className="row">
              <div className="col-lg-6">
                <Form.Group >
                  <Form.Label>Image</Form.Label>
                  <Form.Control
                    type="file"
                    value={formData.image}
                    onChange={handleFileChange} required
                  />
                </Form.Group>
                <Form.Group controlId="description">
                  <Form.Label>Description</Form.Label>
                  <Form.Control
                    type="text"
                    value={formData.description}
                    onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                  />
                </Form.Group>
                <Form.Group controlId="date">
                  <Form.Label>Date</Form.Label>
                  <Form.Control
                    type="date"
                    value={formData.date}
                    onChange={handleInputChange}
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
          <Button variant="primary" onClick={handleSubmit}>
            {isEditing ? 'Update' : 'Add'}
          </Button>
        </Modal.Footer>
      </Modal> */}
    </>
  )
}

export default Blogs
